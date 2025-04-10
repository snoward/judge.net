﻿using System;

namespace Judge.Model.CheckSolution;

public sealed class Task
{
    private string name;
    public long Id { get; private set; }
    public string TestsFolder { get; set; }
    public int TimeLimitMilliseconds { get; set; }
    public int MemoryLimitBytes { get; set; }

    public string Name
    {
        get => this.name;
        set => this.name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime CreationDateUtc { get; private set; }
    public string Statement { get; set; }
    public bool IsOpened { get; set; }
}