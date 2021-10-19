using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonManagementContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonssController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PersonsController(PersonManagementContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// DELETE: api/Persons/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(long id)
        {
            var person = await context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            context.Persons.Remove(person);
            await context.SaveChangesAsync();

            return person;
        }

        /// <summary>
        /// GET: api/Persons/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(long id)
        {
            var person = await context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        /// <summary>
        /// GET: api/Persons.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await context.Persons.ToListAsync();
        }

        /// <summary>
        /// POST: api/Persons.
        /// </summary>
        /// <param name="person">The person.</param>
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            context.Persons.Add(person);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        /// <summary>
        /// PUT: api/Persons/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="person">The person.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            context.Entry(person).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PersonExists(long id)
        {
            return context.Persons.Any(e => e.Id == id);
        }
    }
}