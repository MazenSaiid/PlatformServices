using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using System.Collections;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository platformRepository,IMapper mapper)
        {
           _platformRepository = platformRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllPlatforms")]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            IEnumerable<Platform> platforms = _platformRepository.GetAllPlatforms();
            if (platforms == null)
            {
                return NotFound("No Platforms Found");
            }
            var result = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
            return Ok(result);
        }
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Platform platform = _platformRepository.GetPlatformById(id);
            if (platform == null)
            {
                return NotFound($"No Platform found with below {id}");
            }
            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }
        [HttpPost]
        [Route("CreatePlatform")]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var platform = _mapper.Map<Platform>(platformCreateDto);
                    _platformRepository.CreatePlatform(platform);
                    _platformRepository.SaveChanges();
                    var result = _mapper.Map<PlatformReadDto>(platform);
                    return CreatedAtRoute("GetPlatformById", routeValues: new { id = result.Id }, value: result);
                }
                else
                {
                    return BadRequest("Failed to create a new Platform");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}
