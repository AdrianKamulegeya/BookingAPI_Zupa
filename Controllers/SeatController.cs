using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApi.Models;
using Newtonsoft.Json;
using System;

namespace BookingApi.Controllers
{
    [Route("api/seat")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly BookingContext _context;
        private const int NUMBER_OF_SEATS = 10;

        public SeatController(BookingContext context)
        {
            _context = context;

            if (_context.SeatItems.Count() == 0)
            {
                // Populates db with seats from A1 to J10
                for(int i=0; i < NUMBER_OF_SEATS; i++){
                    for(int j=0; j < NUMBER_OF_SEATS; j++){
                        string seatLetter = ((char) (65 + i)).ToString();
                        string seatNumber = ((j + 1)).ToString();
                        _context.SeatItems.Add(new SeatItem { SeatNumber = seatLetter + seatNumber });
                    }
                }
                _context.SaveChanges();
            }

            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatItem>>> GetSeatItems()
        {
            return await _context.SeatItems.ToListAsync();
        }

        [HttpGet("{seatNumber}")]
        public async Task<ActionResult<SeatItem>> GetSeatItem(string seatNumber)
        {
            var seatItem = await _context.SeatItems.FindAsync(seatNumber);

            if (seatItem == null)
            {
                return NotFound();
            }

            return seatItem;
        }

        [HttpPost]
        public async Task<ActionResult<SeatItem>> PostSeatItem(SeatItem seatItem)
        {

            _context.SeatItems.Add(seatItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeatItem", new { seatNumber = seatItem.SeatNumber }, seatItem);
        }

        [HttpPut("{seatNumber}")]
        public async Task<IActionResult> PutSeatItem(string seatNumber, SeatItem seatItem)
        {
            var requestedSeat = await _context.SeatItems.AsNoTracking().Where(t => t.SeatNumber== seatNumber).FirstAsync();

            if (!seatNumber.Equals(seatItem.SeatNumber))
            {
                return BadRequest();

            }//doesn't allow  null values for email or unique names to be entered
            else if(seatItem.EmailAddress == null || seatItem.UniqueName == null)
            {
                return BadRequest();
            }else if(requestedSeat.Booked) //doesn't allow if seat is already booked
            {    
                return BadRequest();
            }
            
            _context.Entry(seatItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> PutSeatItems([FromBody] dynamic seatItems)
        {
            string json = Convert.ToString(seatItems);

            SeatTransactions transaction = JsonConvert.DeserializeObject<SeatTransactions>(json);
            
            if(transaction.seats.Count > 4){ //doesn't allow more than 4 seats per transaction
                return BadRequest();
            }else{
                foreach(SeatItem seatItem in transaction.seats){
                    var requestedSeat = await _context.SeatItems.AsNoTracking().Where(t => t.SeatNumber== seatItem.SeatNumber).FirstAsync();

                    if(seatItem.EmailAddress == null || seatItem.UniqueName == null) //doesn't allow null values for email or unique names to be entered
                    {
                        return BadRequest();
                    }else if(requestedSeat.Booked)  //doesn't allow if seat is already booked
                    {
                        return BadRequest();
                    }
                    
                    _context.Entry(seatItem).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                
                return Ok();
            }

            
        }

        [HttpDelete("{seatNumber}")]
        public async Task<ActionResult<SeatItem>> DeleteSeatItem(string seatNumber)
        {
            var seatItem = await _context.SeatItems.FindAsync(seatNumber);
            if (seatItem == null)
            {
                return NotFound();
            }

            _context.SeatItems.Remove(seatItem);
            await _context.SaveChangesAsync();

            return seatItem;
        }
        
    }

    
}