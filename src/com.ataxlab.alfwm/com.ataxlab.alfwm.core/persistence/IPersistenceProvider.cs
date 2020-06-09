using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.persistence
{
    
    /// <summary>
    /// deliberately generic specification
    /// hopefully allowing implementers to provide their
    /// own CRUD of whatever type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public interface IPersistenceProvider<TProviderConfiguration, TConfigureResult> 
        where TProviderConfiguration : class, new()
        where TConfigureResult : class, new()
    {
        /// <summary>
        /// support persistable entity semantics
        /// </summary>
        string PersistenceProviderId { get; set; }

        string PersistenceProviderName { get; set; }

        string PersistenceProviderDisplayName { get; set; }

        /// <summary>
        /// support dynamic invocation by specifying the class nae
        /// </summary>
        string PersistenceProviderHostClassName { get; set; }

        /// <summary>
        /// support Activator symaitics
        /// </summary>
        string PersistenceProviderAssemblyName { get; set; }

        TProviderConfiguration ProviderConfiguration { get; set; }

        /// <summary>
        /// managing a context from the configuration left as an exercise to other code
        /// 
        /// one has to be particular about covariance and contravariance
        /// when defining a Configure method for a random persistence
        /// provider implementation. 
        /// 
        /// the thinking seems to be that it should be contravariant
        /// that is, the generic method should onlhy use the generic
        /// method type as an output parameter
        /// 
        /// otherwise your abstract implementations will become twisted
        /// and you will find yourself staring at the prospect of
        /// adding each generic method type parameter to the generic interface definition
        /// an obviously silly proposition
        /// </summary>
        /// <typeparam name="TConfigureResult"></typeparam>
        /// <typeparam name="TProviderConfiguration"></typeparam>
        /// <param name="config"></param>
        /// <param name="configureProviderOperation"></param>
        /// <returns></returns>
        TConfigureResult ConfigureProvider(TProviderConfiguration config);
        TConfigureResult ConfigureProvider(Func<TProviderConfiguration, TConfigureResult> configureProviderOperation);
                                                   

    }
}
