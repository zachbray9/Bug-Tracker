﻿using BugTracker.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Enumerables.EnumConverters
{
    public class StatusOptionsRetriever
    {
        public Dictionary<Status, string> StatusOptionsDictionary { get; set; }

        public StatusOptionsRetriever()
        {
            StatusOptionsDictionary = new Dictionary<Status, string>()
            {
                { Status.ToDo, "To Do" },
                { Status.InProgress, "In Progress"},
                { Status.Done, "Done"}
            };
        }

        public string ConvertStatusEnumToString(Status status)
        {
            if (StatusOptionsDictionary.TryGetValue(status, out var value))
            {
                return value.ToString();
            }

            return String.Empty;
        }

        public Status ConvertStatusStringToEnum(string statusString)
        {
            Status status = StatusOptionsDictionary.FirstOrDefault(d => d.Value == statusString).Key;
            return status;
        }
    }
}