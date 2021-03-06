﻿using System;
using Jenkins;
namespace JenkinsBuilds
{
    public interface IClientFactory
    {
        JenkinsClient GetClient(Uri serverUri);
        JenkinsClient GetClient(string serverUri);
    }
}
