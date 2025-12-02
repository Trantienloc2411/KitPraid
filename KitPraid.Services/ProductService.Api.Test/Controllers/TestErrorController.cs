using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Exceptions;

namespace ProductService.Api.Test.Controllers
{
    [ApiController]
    [Route("test-error")]
    public class TestErrorController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult A() => throw new NotFoundException("NF");

        [HttpGet("bad")]
        public IActionResult B() => throw new BadRequestException("BR");

        [HttpGet("unauth")]
        public IActionResult C() => throw new UnauthorizedException("UA");

        [HttpGet("forbidden")]
        public IActionResult D() => throw new ForbiddenException("FB");

        [HttpGet("conflict")]
        public IActionResult E() => throw new ConflictException("CF");

        [HttpGet("unknown")]
        public IActionResult F() => throw new Exception("ERR");
    }
}