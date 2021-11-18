﻿using System;
using System.Collections.Generic;

public class Mock
{
    private readonly List<string> arguments;

    public float Value
    {
        get;
        private set;
    }

    public string Argument
    {
        get
        {
            return arguments[arguments.Count - 1];
        }
        set
        {
            arguments.Add(value);
        }
    }

    public Mock()
    {
        arguments = new List<string>();
    }

    public Mock CalledWith(string arg)
    {
        if(!arguments.Contains(arg))
        {
            throw new Exception("Not called with argument: " + arg);
        }

        return this;
    }

    public void Returns(float value)
    {
        Value = value;
    }
}

