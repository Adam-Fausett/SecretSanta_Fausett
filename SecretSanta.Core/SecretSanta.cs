using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Core.Extensions;
using SecretSanta.Models;

namespace SecretSanta.Core
{
    public static class SecretSanta
    {
        public static List<Models.SecretSanta> DrawNames(Participant[][] participants)
        {
            var results = new List<Models.SecretSanta>();

            if (participants != null)
            {
                // sort the groups to let the largest groups choose first
                participants = participants.OrderByDescending(x => x.Length).ToArray();

                var usedParticipantIDs = new List<Guid>();

                for (int a = 0; a < participants.Length; ++a)
                {
                    // gather available pool of participants from outside of this group
                    var pool = new List<Participant>();
                    for (int b = 0; b < participants.Length; ++b)
                    {
                        if (b != a)
                        {
                            // shuffle participants[b]
                            // [optional] - makes subsequent draws w/ the same input have diff output
                            participants[b].Shuffle();
                            for (int c = 0; c < participants[b].Length; ++c)
                            {
                                if (!usedParticipantIDs.Contains(participants[b][c].ID))
                                {
                                    pool.Add(participants[b][c]);
                                }
                            }
                        }
                    }

                    // shuffle participants[a]
                    // [optional] - makes subsequent draws w/ the same input have diff output
                    participants[a].Shuffle();

                    // select recipients from the available pool
                    for (int d = 0; d < participants[a].Length; ++d)
                    {
                        var selectedIndex = (pool.Count - 1) - d;
                        var secretSanta = new Models.SecretSanta()
                        {
                            ID = participants[a][d].ID,
                            Name = participants[a][d].Name,
                            Recipient = (selectedIndex >= 0) ? pool[selectedIndex] : null
                        };

                        results.Add(secretSanta);

                        if (secretSanta.Recipient != null)
                        {
                            usedParticipantIDs.Add(secretSanta.Recipient.ID);
                        }
                        // else someone didn't get to play :(
                    }
                }
            }

            return results;
        }
    }
}
