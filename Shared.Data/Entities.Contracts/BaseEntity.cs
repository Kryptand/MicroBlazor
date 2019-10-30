using System;

namespace Shared.Data.Entities.Contracts
{
    public abstract class BaseEntity<TId>:ITrackable
    {
        public TId Id { get; set; }
        public DateTime CreatedOn { get ; set ; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
