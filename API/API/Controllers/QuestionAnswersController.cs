using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Identity;
using API.Models.Identity;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private Random random;

        public QuestionAnswersController(AppDbContext context)
        {
            _context = context;
            random = new Random();
        }

        // GET: api/QuestionAnswers
        [HttpGet]
        [Authorize(Roles="Admin,Server")]
        public async Task<ActionResult<IEnumerable<QuestionAnswers>>> GetQA()
        {
            return await _context.QA.ToListAsync();
        }

        // GET: api/QuestionAnswers/random
        [HttpGet("random")]
        [Authorize(Roles="Admin,Server")]
        public QuestionAnswers GetRandomQA()
        {
            int l = random.Next(0, (_context.QA.Count()));
            return _context.QA.ToList().ElementAtOrDefault(l);
        }

        // GET: api/QuestionAnswers/5
        [HttpGet("{id}")]
        [Authorize(Roles="Admin,Server")]
        public async Task<ActionResult<QuestionAnswers>> GetQuestionAnswers(Guid id)
        {
            var questionAnswers = await _context.QA.FindAsync(id);

            if (questionAnswers == null)
            {
                return NotFound();
            }

            return questionAnswers;
        }

        // PUT: api/QuestionAnswers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> PutQuestionAnswers(Guid id, QuestionAnswers questionAnswers)
        {
            if (id != questionAnswers.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionAnswers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionAnswersExists(id))
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

        // POST: api/QuestionAnswers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<QuestionAnswers>> PostQuestionAnswers(QuestionAnswers questionAnswers)
        {
            _context.QA.Add(questionAnswers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionAnswers", new { id = questionAnswers.Id }, questionAnswers);
        }

        // DELETE: api/QuestionAnswers/5
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<QuestionAnswers>> DeleteQuestionAnswers(Guid id)
        {
            var questionAnswers = await _context.QA.FindAsync(id);
            if (questionAnswers == null)
            {
                return NotFound();
            }

            _context.QA.Remove(questionAnswers);
            await _context.SaveChangesAsync();

            return questionAnswers;
        }

        [Authorize(Roles="Admin,Server")]
        private bool QuestionAnswersExists(Guid id)
        {
            return _context.QA.Any(e => e.Id == id);
        }
    }
}
