using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Abp.Locking
{
    [DependsOn(typeof(AbpKernelModule))]
    public class LockingModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LockingModule).GetAssembly());
        }
    }
}
