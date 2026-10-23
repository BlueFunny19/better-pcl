using System;
using System.Collections.Generic;
using PclHarmony.Contract;

namespace BetterPcl
{
    /// <summary>
    /// Entry point found by PCL-Harmony. This skeleton only proves that the plugin is
    /// loaded and that one declared patch is applied; it changes no launcher behavior.
    /// </summary>
    public sealed class BetterPclPlugin : IDeclarativePlugin
    {
        private static IPluginHost _host;
        private static bool _probeLogged;

        public string Name { get { return "BetterPcl"; } }

        public IEnumerable<PatchSpec> GetPatches(string hostKind, string hostVersion, IPluginHost host)
        {
            _host = host;
            Log("loaded, hostKind=" + hostKind + ", hostVersion=" + hostVersion);

            // Probe: a harmless method that runs at startup, to see the patch take effect.
            // The target is a VB module, so it is a static method on a class named PCL.ModBase.
            return new[]
            {
                new PatchSpec
                {
                    TypeName = "PCL.ModBase",
                    MethodName = "GetTimeMs",
                    HookKind = "postfix",
                    HookTypeName = typeof(BetterPclPlugin).FullName,
                    HookMethodName = "AfterGetTimeMs",
                    TargetParameterTypes = new string[0],
                },
            };
        }

        public static void AfterGetTimeMs()
        {
            try
            {
                if (_probeLogged) return;
                _probeLogged = true;
                Log("probe: PCL.ModBase.GetTimeMs postfix ran");
            }
            catch
            {
                // A hook must never throw into the launcher.
            }
        }

        private static void Log(string message)
        {
            try { if (_host != null) _host.Log("[BetterPcl] " + message); }
            catch { }
        }
    }
}
