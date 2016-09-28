using System.Collections.Generic;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Helpers
{
    /// <summary>
    /// ActionsMapper.
    /// </summary>
    public static class ActionsMapper
    {
        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        public static Dictionary<string, int> Actions { get; set; }

        /// <summary>
        /// Initializes the <see cref="ActionsMapper"/> class.
        /// </summary>
        static ActionsMapper()
        {
            Actions = new Dictionary<string, int>();
            Actions.Add("delete", (int)Domain.Entities.Enums.Actions.Delete);
            Actions.Add("get", (int)Domain.Entities.Enums.Actions.Get);
            Actions.Add("options", (int)Domain.Entities.Enums.Actions.Options);
            Actions.Add("patch", (int)Domain.Entities.Enums.Actions.Patch);
            Actions.Add("post", (int)Domain.Entities.Enums.Actions.Post);
            Actions.Add("put", (int)Domain.Entities.Enums.Actions.Put);
            Actions.Add("any",
                (int)
                    (Domain.Entities.Enums.Actions.Delete | Domain.Entities.Enums.Actions.Get |
                     Domain.Entities.Enums.Actions.Head | Domain.Entities.Enums.Actions.Options |
                     Domain.Entities.Enums.Actions.Patch | Domain.Entities.Enums.Actions.Post |
                     Domain.Entities.Enums.Actions.Put));
        }
    }
}