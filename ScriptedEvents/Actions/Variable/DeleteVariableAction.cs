﻿namespace ScriptedEvents.Actions
{
    using System;

    using ScriptedEvents.API.Enums;
    using ScriptedEvents.API.Interfaces;
    using ScriptedEvents.Structures;
    using ScriptedEvents.Variables;
    using ScriptedEvents.Variables.Interfaces;

    public class DeleteVariableAction : IScriptAction, IHelpInfo
    {
        /// <inheritdoc/>
        public string Name => "DELVARIABLE";

        /// <inheritdoc/>
        public string[] Aliases => new[] { "DEL" };

        /// <inheritdoc/>
        public string[] RawArguments { get; set; }

        /// <inheritdoc/>
        public object[] Arguments { get; set; }

        /// <inheritdoc/>
        public ActionSubgroup Subgroup => ActionSubgroup.Variable;

        /// <inheritdoc/>
        public string Description => "Deletes a previously-defined variable.";

        /// <inheritdoc/>
        public Argument[] ExpectedArguments => new[]
        {
            new Argument("variableName", typeof(IConditionVariable), "The name of the variable.", true),
        };

        /// <inheritdoc/>
        public ActionResponse Execute(Script script)
        {
            if (Arguments.Length < 1) return new(MessageType.InvalidUsage, this, null, (object)ExpectedArguments);
            if (VariableSystem.DefinedVariables.ContainsKey((string)Arguments[0]))
            {
                VariableSystem.DefinedVariables.Remove((string)Arguments[0]);
                return new(true);
            }

            return new(false, $"Invalid variable '{Arguments[0]}'");
        }
    }
}
