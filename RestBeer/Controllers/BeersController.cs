using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestBeer.Managers;
using BeerLib;

namespace RestBeer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : Controller
    {
        private readonly BeersManagers _manager = new BeersManagers();



        // GET: api/<BooksController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Beer> Get([FromQuery] string name, [FromQuery] string sort_by)
        {
            return _manager.GetAll(name, sort_by);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Beer> Get(int id)
        {
            Beer beer = _manager.GetById(id);
            if (beer == null) return NotFound("No such beer, id: " + id);
            return Ok(beer);
        }

        // POST api/<BooksController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Beer> Post([FromBody] Beer value)
        {
            try
            {
                Beer newBeer = _manager.Add(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + newBeer.ID;
                return Created(uri, newBeer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Beer> Put(int id, [FromBody] Beer value)
        {
            try
            {
                Beer updatedBeer = _manager.Update(id, value);
                if (updatedBeer == null) return NotFound("No such beer, id: " + id);
                return Ok(updatedBeer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Beer> Delete(int id)
        {
            Beer deletedBeer = _manager.Delete(id);
            if (deletedBeer == null) return NotFound("No such beer, id: " + id);
            return Ok(deletedBeer);
        }
    }
}
