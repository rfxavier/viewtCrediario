using System;
using ViewT.Crediario.Domain.Main.Enums;
using Version = ViewT.Crediario.Domain.Main.Entities.Version;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders
{
    public class VersionBuilder
    {
        private Guid _versionId = Guid.Empty;
        private DeviceOs _os = DeviceOs.iOS;
        private int _versionNumber = 0;


        public Version Build()
        {
            var version = new Version(_versionId, _os, _versionNumber);

            return version;
        }

        public VersionBuilder WithVersionId(Guid versionId)
        {
            this._versionId = versionId;
            return this;
        }

        public VersionBuilder WithDeviceOs(DeviceOs os)
        {
            this._os = os;
            return this;
        }

        public VersionBuilder WithVersionNumber(int versionNumber)
        {
            this._versionNumber = versionNumber;
            return this;
        }


        public static implicit operator Version(VersionBuilder instance)
        {
            return instance.Build();
        }

    }
}