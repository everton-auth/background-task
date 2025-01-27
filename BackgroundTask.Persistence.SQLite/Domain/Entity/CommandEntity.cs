using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BackgroundTask.Persistence.SQLite.Domain.Entity {
    public class CommandEntity : IEntityTypeConfiguration<CommandEntity> {
        public int ID { get; set; }
        public string CommandName { get; set; }
        public string Payload { get; set; }
        public string Status { get; set; }
        public DateTime ExecutionStartTime { get; set; }
        public Nullable<DateTime> ExecutionEndTime { get; set; }
        public string ErrorMessage { get; set; }

        public void Configure( EntityTypeBuilder<CommandEntity> builder ) {
            builder.HasKey( e => e.ID );
        }
    }
}
