using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspectize;

namespace JenkinsBuilds.Aspects
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NotifyAttribute : MethodBoundaryAspect
    {
        public override bool IsValidOnMember(System.Reflection.MemberInfo memberInfo)
        {
            return typeof(Base.NotifyPropertyChangeBase).IsAssignableFrom(memberInfo.DeclaringType);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            (args.Target as Base.NotifyPropertyChangeBase).RaisePropertyChanged(args.Method.Name.Substring(4));
        }
    }
}
