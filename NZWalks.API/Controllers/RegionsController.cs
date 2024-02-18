using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()

        {

            //Get Data From Database - Domain Models
            var regions = dbContext.Regions.ToList();

            // map Domain Models to DTOs
            var regionDtos = new List<RegionDto>();

            foreach (var regionDomain in regionDtos)
            {

                regionDtos.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });


                
            }

            //var regions = new List<Region>

            //{

            //    new Region

            //   {

            //        Id = Guid.NewGuid(),
            //        Name = "Auckland Region",
            //        Code = "AKL",
            //        RegionImageUrl="https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/auckland/auckland-region.jpg",

            //   },

            //    new Region

            //    {

            //        Id= Guid.NewGuid(),
            //        Name = "Wellington Region",
            //        Code = "WLG",
            //        RegionImageUrl="https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/wellington/wellington-region.jpg",

            //    }


            //};

            return Ok(regions);


        }

        [HttpGet]
        [Route("{Id:Guid}")]

        public IActionResult GetById([FromRoute]Guid Id)

        {

            var regionDomain = dbContext.Regions.FirstOrDefault(x=> x.Id == Id);

            if (regionDomain == null)

            {

                return NotFound();

            }

            var regionDto = new RegionDto

            {

                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl

            };

            //Return Dto back to the client
            return Ok(regionDomain);

            


        }
        
        [HttpPost]

        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)

        {

            var regionDomainModel = new Region

            {

                Id = Guid.NewGuid(),
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl

            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domain Model to Dto

            var regionDto = new RegionDto

            {

                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };

            return CreatedAtAction(nameof(GetById), new { Id = regionDomainModel.Id }, regionDomainModel);

        }
    }
}
