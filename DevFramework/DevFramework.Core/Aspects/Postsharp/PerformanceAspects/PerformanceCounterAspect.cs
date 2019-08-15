using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace DevFramework.Core.Aspects.Postsharp.PerformanceAspects
{
    [Serializable]
   public class PerformanceCounterAspect: OnMethodBoundaryAspect
    {
        private int _interval;
        [NonSerialized]
        Stopwatch _stopWatch;
        public PerformanceCounterAspect(int interval = 5)
        {
            _interval = interval;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            _stopWatch = Activator.CreateInstance<Stopwatch>();
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            _stopWatch.Start();
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _stopWatch.Stop();
            if (_stopWatch.Elapsed.TotalSeconds>_interval)
            {
                Debug.WriteLine("Performance: {0}.{1}->>{2}", args.Method.DeclaringType.FullName, args.Method.Name,_stopWatch.Elapsed.TotalSeconds);//e-mail,logging
            }
            _stopWatch.Reset();
            base.OnExit(args);
        }
    }
}
