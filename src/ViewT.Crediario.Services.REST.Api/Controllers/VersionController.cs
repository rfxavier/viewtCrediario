using System.Web.Http;
using ViewT.Crediario.Domain.Core.Interfaces;
using ViewT.Crediario.Domain.Main.Commands.Results;
using ViewT.Crediario.Domain.Main.Enums;
using ViewT.Crediario.Domain.Main.Interfaces;

namespace ViewT.Crediario.Services.REST.Api.Controllers
{
    [RoutePrefix("Version")]
    public class VersionController : BaseController
    {
        private readonly IVersionRepository _versionRepository;

        public VersionController(
            IVersionRepository versionRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _versionRepository = versionRepository;
        }

        [HttpGet]
        [Route("GetMinimumVersion/v1/{os:int}")]
        public IHttpActionResult GetByOs(int os)
        {
            GetMinimumVersionCommandResult result = _versionRepository.GetByOs(DeviceOs.FromValue(os));

            return Response(result);
        }
    }
}