using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactList.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class PersonController : ControllerBase
    {
        private static readonly List<Person> people = new List<Person>();

        [HttpGet]
        public IActionResult GetAllPeople()
        {
            return Ok(people);
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody] Person newPerson)
        {
            if (newPerson.Id != null && newPerson.Email != null && newPerson.Email != "")
            {
                people.Add(newPerson);
                return Ok(newPerson);
            }
            return BadRequest("Invalid input(e.g.required field missing or empty)");
        }

        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeletePerson(int personId)
        {
            if (personId >= 0 && personId < people.Count)
            {
                // missing if for Person
                // missing Error message 404
                people.RemoveAt(personId);
                return NoContent();
            }
            return BadRequest("Invalid ID supplied");
        }

        [HttpGet]
        [Route("{findByName}", Name = "GetSpecificPerson")]
        public IActionResult GetPesonByName(string name)
        {
            if ( name != null && name != "")
            {
                foreach (Person p in people)
                {
                    if ( p.FirstName.Equals(name) || p.LastName.Equals(name))
                    {
                        return Ok(p);
                    }
                }            }
            return BadRequest("Invalid or missing name");
        }
    }
}
