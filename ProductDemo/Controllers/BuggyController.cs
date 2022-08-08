using InfraStructure.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductDemo.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductDemo.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext Context)
        {
            _context = Context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Product.Find(60);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Product.Find(60);
            var thingToReturn = thing.ToString();
            return Ok(new ApiResponse(500));
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
