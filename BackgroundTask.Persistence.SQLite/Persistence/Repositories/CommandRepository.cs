using BackgroundTask.Interfaces;
using BackgroundTask.Persistence.SQLite.Domain.Entity;
using BackgroundTask.Persistence.SQLite.Domain.Enums;
using BackgroundTask.Persistence.SQLite.Persistence.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Persistence.SQLite.Persistence.Repositories {
    public class CommandRepository : ICommandRepository {
        private readonly IApplicationDBContext _dbcontext;

        public CommandRepository( IApplicationDBContext dbContext ) {
            _dbcontext = dbContext;
        }


        public async Task<CommandEntity> RegisterCommand( IBackgroundCommand command , CancellationToken cancellationToken ) {
            var commandHistory = new CommandEntity {
                CommandName = command.GetType().Name ,
                Payload = JsonSerializer.Serialize( command ) ,
                Status = StatusCommandEnum.PENDING.ToString() ,
                ExecutionStartTime = DateTime.Now ,
            };


            await _dbcontext.Commands.AddAsync( commandHistory , cancellationToken );
            await _dbcontext.SaveChangesAsync( cancellationToken );

            return commandHistory;
        }

        public async Task<CommandEntity> FinalizeCommand( CommandEntity command , CancellationToken cancellationToken ) {
            var DBCommand = await _dbcontext.Commands.Where( c => c.ID == command.ID ).FirstOrDefaultAsync( cancellationToken );
            if( DBCommand == null )
                throw new Exception( "O comando não foi encontrado" );

            DBCommand.Status = command.Status;
            DBCommand.ErrorMessage = command.ErrorMessage;
            DBCommand.ExecutionEndTime = DateTime.Now;

            _dbcontext.Commands.Update( DBCommand );
            await _dbcontext.SaveChangesAsync( cancellationToken );

            return DBCommand;
        }

        public async Task<List<CommandEntity>> GetCommands( Expression<Func<CommandEntity , bool>> predicate , CancellationToken cancellationToken , bool untrack = false ) {

            var query = _dbcontext.Commands.Where( predicate ).AsQueryable();
            if( untrack )
                query = query.AsNoTracking();

            var listOfCommands = await query.ToListAsync( cancellationToken );
            return listOfCommands;
        }
    }
}