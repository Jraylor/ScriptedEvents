﻿namespace ScriptedEvents.Actions
{
    using System;

    using ScriptedEvents.API.Enums;
    using ScriptedEvents.API.Extensions;
    using ScriptedEvents.API.Features;
    using ScriptedEvents.API.Interfaces;
    using ScriptedEvents.Structures;
    using ScriptedEvents.Variables;

    public class LocalSaveAction : IScriptAction, IHelpInfo
    {
        /// <inheritdoc/>
        public string Name => "LOCAL";

        /// <inheritdoc/>
        public string[] Aliases => Array.Empty<string>();

        /// <inheritdoc/>
        public string[] RawArguments { get; set; }

        /// <inheritdoc/>
        public object[] Arguments { get; set; }

        /// <inheritdoc/>
        public ActionSubgroup Subgroup => ActionSubgroup.Variable;

        /// <inheritdoc/>
        public string Description => "Saves a new variable. Saved variables can be used in THIS script only, and are reset when the round ends.";

        /// <inheritdoc/>
        public Argument[] ExpectedArguments => new[]
        {
            new Argument("variableName", typeof(string), "The name of the new variable. Braces will be added automatically if not provided.", true),
            new Argument("value", typeof(object), "The value to store. Variables & Math are supported.", true),
        };

        /// <inheritdoc/>
        public ActionResponse Execute(Script script)
        {
            string varName = RawArguments[0];
            string input = Arguments.JoinMessage(1);

            input = VariableSystem.ReplaceVariables(input, script);

            try
            {
                float value = (float)ConditionHelperV2.Math(input);
                script.AddVariable(varName, "User-defined variable.", value.ToString());
                return new(true);
            }
            catch
            {
            }

            script.AddVariable(varName, "User-defined variable.", input);

            return new(true);
        }
    }
}
