using System;

namespace Shared.Data.DataClasses.Contracts
{
    public interface ITrackable
    {
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }
        DateTime ModifiedOn { get; set; }
        string ModifiedBy { get; set; }
    }
}
