// Guids.cs
// MUST match guids.h
using System;

namespace MaciejNowak.JenkinsBuilds
{
    static class GuidList
    {
        public const string guidJenkinsBuildsPkgString = "9408590f-6811-4ff2-9580-e0d89ecfc9f7";
        public const string guidJenkinsBuildsCmdSetString = "64d5ac09-6076-428d-9d3d-f90900649c53";

        public static readonly Guid guidJenkinsBuildsCmdSet = new Guid(guidJenkinsBuildsCmdSetString);
    };
}