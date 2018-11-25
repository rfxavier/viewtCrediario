using System;
using ViewT.Crediario.Domain.Core.Models;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Main.Entities
{
    public class Version : Entity
    {
        protected Version() {}

        public Version(Guid versionId, DeviceOs os, int versionNumber)
        {
            VersionId = versionId;
            Os = os;
            VersionNumber = versionNumber;
            Active = true;
            DateCreated = DateTime.Now;
        }

        public Guid VersionId { get; private set; }
        public DeviceOs Os { get; private set; }
        public int VersionNumber { get; private set; }
    }
}