﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Swashbuckle.SwaggerGen.Generator;

namespace GeekLearning.SwashbuckleExtensions
{
    public class ExamplesBuilder
    {
        public ExamplesBuilder()
        {
            this.Examples = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>(); 
        }

        public ControllerContext<TController> ForController<TController>()
        {
            return new ControllerContext<TController>(this.Examples);
        }

        public ControllerContext<TController> ForController<TController>(Action<ControllerContext<TController>> builder)
        {
            var context =  new ControllerContext<TController>(this.Examples);
            builder(context);
            return context;
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> Examples { get; }

        public class ControllerContext<TController>
        {
            private Dictionary<string, Dictionary<string, Dictionary<string, object>>> examples;

            public ControllerContext(Dictionary<string, Dictionary<string, Dictionary<string, object>>> examples)
            {
                this.examples = examples;
            }

            public ActionContext<TReturn> ForAction<TReturn>(Expression<Func<TController, TReturn>> action, Action<ActionContext<TReturn>> builder)
            {
                var context = ForAction(action);
                builder(context);
                return context;
            }


            public ActionContext<TReturn> ForAction<TReturn>(Expression<Func<TController, TReturn>> action)
            {
                var methodCall = ((MethodCallExpression)action.Body);

                var actionId = methodCall.Method.DeclaringType.FullName + "." + methodCall.Method.Name;

                Dictionary<string, Dictionary<string, object>> actionResponses;

                if (!examples.TryGetValue(actionId, out actionResponses))
                {
                    actionResponses = new Dictionary<string, Dictionary<string, object>>();
                    examples[actionId] = actionResponses;
                }

                return new ActionContext<TReturn>(actionResponses);
            }


            public ActionContext<TReturn> ForAction<TReturn>(Expression<Func<TController, Task<TReturn>>> action, Action<ActionContext<TReturn>> builder)
            {
                var context = ForAction(action);
                builder(context);
                return context;
            }

            public ActionContext<TReturn> ForAction<TReturn>(Expression<Func<TController, Task<TReturn>>> action)
            {
                var methodCall = ((MethodCallExpression)action.Body);

                var actionId = methodCall.Method.DeclaringType.FullName + "." + methodCall.Method.Name;

                Dictionary<string, Dictionary<string, object>> actionResponses;

                if (!examples.TryGetValue(actionId, out actionResponses))
                {
                    actionResponses = new Dictionary<string, Dictionary<string, object>>();
                    examples[actionId] = actionResponses;
                }

                return new ActionContext<TReturn>(actionResponses);
            }

            public class ActionContext<TReturn>
            {
                private Dictionary<string, Dictionary<string, object>> actionResponses;

                public ActionContext(Dictionary<string, Dictionary<string, object>> actionResponses)
                {
                    this.actionResponses = actionResponses;
                }

                public ActionContext<TReturn> Json(TReturn data, string status = "200")
                {
                    Dictionary<string, object> statusResponses;

                    if (!actionResponses.TryGetValue(status, out statusResponses))
                    {
                        statusResponses = new Dictionary<string, object>();
                        actionResponses[status] = statusResponses;
                    }

                    statusResponses.Add("application/json", data);
                    return this;
                }
            }
        }

    }
}
