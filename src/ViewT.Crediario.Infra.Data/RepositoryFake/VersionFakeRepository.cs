using ViewT.Crediario.Domain.Main.Commands.Results;
using ViewT.Crediario.Domain.Main.Enums;
using ViewT.Crediario.Domain.Main.Interfaces;

namespace ViewT.Crediario.Infra.Data.RepositoryFake
{
    public class VersionFakeRepository : IVersionRepository
    {
        GetMinimumVersionCommandResult IVersionRepository.GetByOs(DeviceOs os)
        {
            GetMinimumVersionCommandResult version = new GetMinimumVersionCommandResult() { MinimumVersion = 0 };

            if (os == DeviceOs.iOS)
            {
                version = new GetMinimumVersionCommandResult() {MinimumVersion = 1};
            }
            else if (os == DeviceOs.Android)
            {
                version = new GetMinimumVersionCommandResult() { MinimumVersion = 2 };
            }

            return version;
        }
    }
}