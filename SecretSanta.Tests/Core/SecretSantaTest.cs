using System.Collections.Generic;
using System.Linq;
using SecretSanta.Models;
using Xunit;

namespace SecretSanta.Tests.Core
{
    public class SecretSantaTest
    {
        protected bool ValidateSelections(Participant[][] participants, List<SecretSanta.Models.SecretSanta> results)
        {
            // check that that no one is assigned to someone else in their group (including themselves)
            foreach (var participantGroup in participants)
            {
                foreach (var participantA in participantGroup)
                {
                    foreach (var participantB in participantGroup)
                    {
                        Assert.Empty(results.Where(r => r.ID == participantA.ID).Where(r => r?.Recipient?.ID == participantB.ID));
                    }
                }
            }

            return true;
        }

        [Fact]
        public void Common_Problem_Case_2x2x1()
        {
            // Arrange
            var participants = new[]
            {
                new[]
                {
                    new Participant { Name = "3"},
                    new Participant { Name = "4"}
                },
                new[]
                {
                    new Participant { Name = "5"},
                    new Participant { Name = "6"}
                },
                new[]
                {
                    new Participant { Name = "7"}
                }
            };

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.NotEmpty(results);
            Assert.Empty(results.Where(r => r.Recipient == null));
            Assert.True(ValidateSelections(participants, results));
        }

        [Fact]
        public void Common_Problem_Case_3x2x2()
        {
            // Arrange
            var participants = new[]
            {
                new[]
                {
                    new Participant { Name = "1"},
                    new Participant { Name = "2"},
                    new Participant { Name = "3"}
                },
                new[]
                {
                    new Participant { Name = "3"},
                    new Participant { Name = "4"}
                },
                new[]
                {
                    new Participant { Name = "5"},
                    new Participant { Name = "6"}
                }
            };

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.NotEmpty(results);
            Assert.Empty(results.Where(r => r.Recipient == null));
            Assert.True(ValidateSelections(participants, results));
        }

        [Fact]
        public void Common_Problem_Case_3x2x2x1()
        {
            // Arrange
            var participants = new[]
            {
                new[]
                {
                    new Participant { Name = "1"},
                    new Participant { Name = "2"},
                    new Participant { Name = "3"}
                },
                new[]
                {
                    new Participant { Name = "3"},
                    new Participant { Name = "4"}
                },
                new[]
                {
                    new Participant { Name = "5"},
                    new Participant { Name = "6"}
                },
                new[]
                {
                    new Participant { Name = "7"}
                }
            };

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.NotEmpty(results);
            Assert.Empty(results.Where(r => r.Recipient == null));
            Assert.True(ValidateSelections(participants, results));
        }

        [Fact]
        public void Null_Set()
        {
            // Arrange
            Participant[][] participants = null;

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void Empty_Set()
        {
            // Arrange
            Participant[][] participants = new Participant[0][];

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.Empty(results);
        }
    }
}
