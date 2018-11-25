using ViewT.Crediario.Domain.Main.Commands.Results;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Main.Interfaces
{
    public interface IVersionRepository
    {
        GetMinimumVersionCommandResult GetByOs(DeviceOs os);
    }
}