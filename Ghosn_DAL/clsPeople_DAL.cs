using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_DAL
{
    public class clsPeople_DAL
    {
        private static string _connectionString = clsSettings.connectionString;

        public class PersonObject
        {
            public int PersonID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

            public PersonObject(int personID, string firstName, string lastName, string email)
            {
                PersonID = personID;
                FirstName = firstName;
                LastName = lastName;
                Email = email;
            }
        }

        public static List<PersonObject> GetAllPeople()
        {
            var people = new List<PersonObject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM People";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            people.Add(new PersonObject(
                                reader.GetInt32("PersonID"),
                                reader.GetString("FirstName"),
                                reader.GetString("LastName"),
                                reader.GetString("Email")
                            ));
                        }
                    }
                }
            }
            return people;
        }

        public static PersonObject? GetPersonById(int personId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM People WHERE PersonID = @PersonID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PersonObject(
                                reader.GetInt32("PersonID"),
                                reader.GetString("FirstName"),
                                reader.GetString("LastName"),
                                reader.GetString("Email")
                            );
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddPerson(PersonObject person)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO People (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", person.LastName);
                    cmd.Parameters.AddWithValue("@Email", person.Email);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool UpdatePerson(PersonObject person)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE People SET FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE PersonID = @PersonID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                    cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", person.LastName);
                    cmd.Parameters.AddWithValue("@Email", person.Email);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool DeletePerson(int personId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM People WHERE PersonID = @PersonID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
