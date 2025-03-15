using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class PersonDTO
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class clsPeople_BLL
    {
        public static List<PersonDTO> GetAllPeople()
        {
            var peopleObjects = clsPeople_DAL.GetAllPeople();
            return peopleObjects.Select(ConvertToDTO).ToList();
        }

        public static PersonDTO? GetPersonById(int id)
        {
            var personObject = clsPeople_DAL.GetPersonById(id);
            return personObject != null ? ConvertToDTO(personObject) : null;
        }

        public static int AddPerson(PersonDTO dto)
        {
            var personObject = ConvertToDALObject(dto);
            return clsPeople_DAL.AddPerson(personObject);
        }

        public static bool UpdatePerson(PersonDTO dto)
        {
            var personObject = ConvertToDALObject(dto);
            return clsPeople_DAL.UpdatePerson(personObject);
        }

        public static bool DeletePerson(int id)
        {
            return clsPeople_DAL.DeletePerson(id);
        }

        // Conversion methods
        private static PersonDTO ConvertToDTO(clsPeople_DAL.PersonObject obj)
        {
            return new PersonDTO
            {
                PersonID = obj.PersonID,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Email = obj.Email
            };
        }

        private static clsPeople_DAL.PersonObject ConvertToDALObject(PersonDTO dto)
        {
            return new clsPeople_DAL.PersonObject(dto.PersonID, dto.FirstName, dto.LastName, dto.Email);
        }
    }
}
