using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoterListCrudAPI.Data;
using VoterListCrudAPI.Models;

namespace VoterListCrudAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotersController : Controller
    {
        private readonly VoterListCrudAPIDbContext _VoterListCrudAPIDbContext;
        public VotersController(VoterListCrudAPIDbContext voterListCrudAPIDbContext)
        {
            _VoterListCrudAPIDbContext = voterListCrudAPIDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVoters()
        {
            var voters = await _VoterListCrudAPIDbContext.Voters.ToListAsync();
            return Ok(voters);
        }
        [HttpPost]
        public async Task<IActionResult> AddVoter([FromBody] Voter voterRequest)
        {
            voterRequest.Id = Guid.NewGuid();
            await _VoterListCrudAPIDbContext.Voters.AddAsync(voterRequest);
            await _VoterListCrudAPIDbContext.SaveChangesAsync();
            return Ok(voterRequest);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetVoter([FromRoute] Guid id)
        {
            var voter = await _VoterListCrudAPIDbContext.Voters.FirstOrDefaultAsync(x  => x.Id == id);

            if(voter == null)
            {
                return NotFound();
            }

            return Ok(voter);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateVoter([FromRoute] Guid id, Voter updateVoter)
        {
            var voter = await _VoterListCrudAPIDbContext.Voters.FindAsync(id);

            if(voter == null)
            {
                return NotFound();
            }

            voter.Name = updateVoter.Name;
            voter.Email = updateVoter.Email;
            voter.PhoneNumber = updateVoter.PhoneNumber;

            await _VoterListCrudAPIDbContext.SaveChangesAsync();

            return Ok(voter);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteVoter([FromRoute] Guid id)
        {
            var voter = await _VoterListCrudAPIDbContext.Voters.FindAsync(id);

            if(voter == null)
            {
                return NotFound();
            }

            _VoterListCrudAPIDbContext.Voters.Remove(voter);
            await _VoterListCrudAPIDbContext.SaveChangesAsync();
            return Ok(voter);

        }
    }
}
