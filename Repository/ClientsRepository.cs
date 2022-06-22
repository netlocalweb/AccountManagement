using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Repository
{


    public class ClientsRepository : IClientsRepository
    {

        protected RepositoryContext RepositoryContext;

        public ClientsRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        //Method CREATE
        public void CreateRecord(Clients client, out string ErrorMessage)
        {
            string errorMessage = string.Empty;


            //Email - Phone - Username Validation
            if (DuplicateValidation(client, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;

            }
            else if (IsValidEmail(client, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;

            }
            else if (ValidatePassword(client, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;
            }
            else
            {
                ErrorMessage = "Client added to database";
                CreatePasswordHash(client.Password, out byte[] passwordHash, out byte[] passwordSalt);
                client.PasswordHash = passwordHash;
                client.PasswordSalt = passwordSalt;
                RepositoryContext.Clients.Add(client);
            }

        }
        //Register Method for Authentication
        public void Register(Clients client, out string ErrorMessage)
        {
            string errorMessage = string.Empty;


            //Email - Phone - Username Validation
            if (DuplicateValidation(client, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;

            }
            else if (IsValidEmail(client, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;

            }
            else if (ValidatePassword(client, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;
            }
            else
            {
                ErrorMessage = "Client registered sucefully";
                CreatePasswordHash(client.Password, out byte[] passwordHash, out byte[] passwordSalt);
                client.PasswordHash = passwordHash;
                client.PasswordSalt = passwordSalt;
                RepositoryContext.Clients.Add(client);
            }

        }

        //Method that generates password Hash
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        //Login Validation method
        public void LoginValidation(string username, string password, out string ErrorMessage, out Clients client)
        {
            var obj = RepositoryContext.Clients.Where(x => x.Username == username).FirstOrDefault();
            if (obj == null)
            {
                ErrorMessage = "User not found!";
                client = null;
            }
            else if (!VerifyPasswordHash(password, obj.PasswordHash, obj.PasswordSalt))
            {
                ErrorMessage = "Wrong password!";
                client = null;
            }
            else
            {
                ErrorMessage = "User Logged In!";
                client = obj;

            }
        }

        //Verify Password Method
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }

        }

        //Method GETALL
        public IEnumerable<Clients> GetAllRecords()
        {
            var testAll = RepositoryContext.Clients;
            return (IEnumerable<Clients>)testAll;

        }
        //Method GETBYID
        public Clients GetRecordById(int id)
        {
            var client = RepositoryContext.Clients.Where(x => x.Id == id).FirstOrDefault();
            return client;
        }

        //Method DELETE
        public void RemoveRecord(int id, out bool check)
        {
            var client = RepositoryContext.Clients.Where(x => x.Id == id).FirstOrDefault();
            if(client == null)
            {
                check = false;
            }
            else
            {
                check = true;
                RepositoryContext.Clients.Remove(client);
            }
            
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }

        //Method UPDATE
        public void UpdateRecord(int id, Clients clients, out string ErrorMessage)
        {
            string errorMessage = string.Empty;
            var clientCheck = RepositoryContext.Clients.Where(x => x.Id == id).FirstOrDefault();

            //Email - Phone - Username Validation
            if(clientCheck == null)
            {
                ErrorMessage = "There is no Client with this ID in Database";
            }
            else if (DuplicateValidation(clients, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;

            }
            else if (IsValidEmail(clients, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;

            }
            else if (ValidatePassword(clients, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;
            }
            else
            {
                ErrorMessage = "Client updated";
                var client = RepositoryContext.Clients.Where(x => x.Id == id).FirstOrDefault();
                client.FirstName = clients.FirstName;
                client.LastName = clients.LastName;
                client.Birthdate = clients.Birthdate;
                client.Email = clients.Email;
                client.Phone = clients.Phone;
                client.DateModified = DateTime.Now;
                client.Username = clients.Username;
                client.Password = clients.Password;
            }


        }

        //Duplicate Records Validation Method
        public bool DuplicateValidation(Clients client, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            var email = RepositoryContext.Clients.Where(x => x.Email == client.Email).FirstOrDefault();
            var phone = RepositoryContext.Clients.Where(x => x.Phone == client.Phone).FirstOrDefault();
            var username = RepositoryContext.Clients.Where(x => x.Username == client.Username).FirstOrDefault();

            if (email != null)
            {
                ErrorMessage = "Email already exists in database! Record NOT added to database.";
                return false;
            }
            else if (phone != null)
            {
                ErrorMessage = "Phone number already exists in database! Record NOT added to database.";
                return false;
            }
            else if (username != null)
            {
                ErrorMessage = "Username already exists in database! Record NOT added to database.";
                return false;
            }
            else
            {
                return true;
            }

        }
        //Email validation method
        public bool IsValidEmail(Clients client, out string ErrorMessage)
        {
            string email = client.Email;
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                ErrorMessage = "Email is correct! Record NOT added to database.";
                return true;
            }
            else
            {
                ErrorMessage = "Email is incorrect! Record NOT added to database.";
                return false;
            }

        }

        //Password Validation Method
        private bool ValidatePassword(Clients client, out string ErrorMessage)
        {
            var input = client.Password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input) || !hasUpperChar.IsMatch(input) || !hasMiniMaxChars.IsMatch(input) || !hasNumber.IsMatch(input) || !hasNumber.IsMatch(input) || !hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password must contain at least one uppercase and lowercase letter,one special charcter, one number and at least 8 or more characters";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
