using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNice.Business.DTO;
using NNice.Business.Services;

namespace NNice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var rooms = await _roomService.GetAllAsync();
            if (rooms == null || rooms.Count() == 0)
            {
                return NotFound(new ResponseObject()
                {
                    Success = false,
                    Message = "Can not find the rooms",
                    Code = HttpStatusCode.OK
                });
            }

            return Ok(new ResponseObject<RoomDTO>()
            {
                data = rooms
            });
        }

        // GET: api/Room/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            if (room != null)
                return Ok(new ResponseObject<RoomDTO>
                {
                    data = new List<RoomDTO>() { room }
                });
            return NotFound(new ResponseObject()
            {
                Success = false,
                Message = $"Can not find the room {id}",
                Code = HttpStatusCode.NotFound
            });
        }

        // POST: api/Room
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RoomDTO input)
        {
            if (input.Capacity == 0)
            {
                return NotFound(new ResponseObject()
                {
                    Success = false,
                    Message = "the capacity can not equal 0",
                    Code = HttpStatusCode.NotFound
                });
            }

            await _roomService.CreateAsync(input);
            return Ok(new ResponseObject());
        }

        // PUT: api/Room/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] RoomDTO input)
        {
            await _roomService.UpdateAsync(input, id);
            return Ok(new ResponseObject());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _roomService.DeleteAsync(id);
            return Ok(new ResponseObject());
        }
    }
}
