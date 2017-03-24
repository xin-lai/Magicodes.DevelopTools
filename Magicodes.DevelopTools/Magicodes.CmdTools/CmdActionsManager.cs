using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools
{
    public class CmdActionsManager
    {
        public static Dictionary<Type, ICmdAction> Actions = new Dictionary<Type, ICmdAction>();

        public static void Init()
        {
            var types = (typeof(CmdActionsManager)).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICmdAction))).ToArray();
            foreach (var item in types)
            {
                var cmdAction = (ICmdAction)Activator.CreateInstance(item);
                Actions.Add(item, cmdAction);
            }
        }

        public static void ExecuteAction(object args)
        {
            var type = args.GetType();
            if (Actions.ContainsKey(type))
            {
                Actions[type].Execute(args);
            }
            else
            {
                Console.WriteLine("未找到相应命令，请重试！");
            }
        }
    }
}
