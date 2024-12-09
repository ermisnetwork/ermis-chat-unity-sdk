using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisCommand : IStateLoadableFrom<CommandInternalDTO, ErmisCommand>
    {
        /// <summary>
        /// Arguments help text, shown in commands auto-completion
        /// </summary>
        public string Args { get; private set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// Description, shown in commands auto-completion
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Unique command name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Set name used for grouping commands
        /// </summary>
        public string Set { get; private set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        public System.DateTimeOffset? UpdatedAt { get; private set; }

        ErmisCommand IStateLoadableFrom<CommandInternalDTO, ErmisCommand>.LoadFromDto(CommandInternalDTO dto, ICache cache)
        {
            Args = dto.Args;
            CreatedAt = dto.CreatedAt;
            Description = dto.Description;
            Name = dto.Name;
            Set = dto.Set;
            UpdatedAt = dto.UpdatedAt;

            return this;
        }
    }
}