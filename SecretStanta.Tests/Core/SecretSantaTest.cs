using NUnit.Framework;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretStanta.Tests.Core
{
    [TestFixture()]
    public class SecretSantaTest
    {
        protected bool ValidateSelections(Participant[][] participants, List<SecretSanta.Models.SecretSanta> results)
        {
            // check that that no one is assigned to someone else in their group (including themselves)
            for (int a = 0; a < participants.Length; ++a)
            {
                for (int b = 0; b < participants[a].Length; ++b)
                {
                    for (int c = 0; c < participants[a].Length; ++c)
                    {
                        Assert.IsEmpty(results.Where(r => r.ID == participants[a][b].ID).Where(r => r?.Recipient?.ID == participants[a][c].ID));
                    }
                }
            }

            return true;
        }

        [Test()]
        public void Common_Problem_Case_2x2x1()
        {
            // Arrange
            Participant[][] participants = new Participant[][]
            {
                new Participant[]
                {
                    new Participant { Name = "3"},
                    new Participant { Name = "4"}
                },
                new Participant[]
                {
                    new Participant { Name = "5"},
                    new Participant { Name = "6"}
                },
                new Participant[]
                {
                    new Participant { Name = "7"}
                }
            };

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.IsNotEmpty(results);
            Assert.IsEmpty(results.Where(r => r.Recipient == null));
            Assert.IsTrue(ValidateSelections(participants, results));
        }

        [Test()]
        public void Common_Problem_Case_3x2x2()
        {
            // Arrange
            Participant[][] participants = new Participant[][]
            {
                new Participant[]
                {
                    new Participant { Name = "1"},
                    new Participant { Name = "2"},
                    new Participant { Name = "3"}
                },
                new Participant[]
                {
                    new Participant { Name = "3"},
                    new Participant { Name = "4"}
                },
                new Participant[]
                {
                    new Participant { Name = "5"},
                    new Participant { Name = "6"}
                }
            };

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.IsNotEmpty(results);
            Assert.IsEmpty(results.Where(r => r.Recipient == null));
            Assert.IsTrue(ValidateSelections(participants, results));
        }

        [Test()]
        public void Common_Problem_Case_3x2x2x1()
        {
            // Arrange
            Participant[][] participants = new Participant[][] 
            {
                new Participant[]
                {
                    new Participant { Name = "1"},
                    new Participant { Name = "2"},
                    new Participant { Name = "3"}
                },
                new Participant[]
                {
                    new Participant { Name = "3"},
                    new Participant { Name = "4"}
                },
                new Participant[]
                {
                    new Participant { Name = "5"},
                    new Participant { Name = "6"}
                },
                new Participant[]
                {
                    new Participant { Name = "7"}
                }
            };

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.IsNotEmpty(results);
            Assert.IsEmpty(results.Where(r => r.Recipient == null));
            Assert.IsTrue(ValidateSelections(participants, results));
        }

        [Test()]
        public void Null_Set()
        {
            // Arrange
            Participant[][] participants = null;

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.IsEmpty(results);
        }

        [Test()]
        public void Empty_Set()
        {
            // Arrange
            Participant[][] participants = new Participant[0][];

            // Act
            var results = SecretSanta.Core.SecretSanta.DrawNames(participants);

            // Assert
            Assert.IsEmpty(results);
        }
    }
}
