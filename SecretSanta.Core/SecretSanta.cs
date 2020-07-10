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

            if (participants == null) return results;

            participants.Shuffle();
            // sort the groups to let the largest groups choose first
            participants = participants.OrderByDescending(x => x.Length).ToArray();

            var usedParticipantIDs = new HashSet<Guid>();

            for (var a = 0; a < participants.Length; ++a)
            {
                // gather available pool of participants from outside of this group
                var pool = new List<Participant>();
                for (var b = 0; b < participants.Length; ++b)
                {
                    if (b == a) continue;

                    // shuffle participants[b]
                    // [optional] - makes subsequent draws w/ the same input have diff output
                    participants[b].Shuffle();

                    pool.AddRange(participants[b].Where(participant => !usedParticipantIDs.Contains(participant.ID)));
                }

                // shuffle participants[a]
                // [optional] - makes subsequent draws w/ the same input have diff output
                participants[a].Shuffle();

                // select recipients from the available pool
                for (var c = 0; c < participants[a].Length; ++c)
                {
                    var selectedIndex = (pool.Count - 1) - c;
                    var secretSanta = new Models.SecretSanta()
                    {
                        ID = participants[a][c].ID,
                        Name = participants[a][c].Name,
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

            return results;
        }
    }
}
