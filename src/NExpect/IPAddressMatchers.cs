using System;
using System.Net;
using System.Net.Sockets;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers for strings that should be ip addresses
/// </summary>
public static class IpAddressMatchers
{
    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <returns></returns>
    public static IMore<string> IpAddress(
        this IAn<string> an
    )
    {
        return an.IpAddress(NULL_STRING);
    }

    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<string> IpAddress(
        this IAn<string> an,
        string customMessage
    )
    {
        return an.IpAddress(() => customMessage);
    }

    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<string> IpAddress(
        this IAn<string> an,
        Func<string> customMessageGenerator
    )
    {
        return AddIpMatcher(
            an,
            null,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <returns></returns>
    public static IMore<string> Ipv4Address(
        this IAn<string> an
    )
    {
        return an.Ipv4Address(NULL_STRING);
    }

    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<string> Ipv4Address(
        this IAn<string> an,
        string customMessage
    )
    {
        return an.Ipv4Address(() => customMessage);
    }

    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<string> Ipv4Address(
        this IAn<string> an,
        Func<string> customMessageGenerator
    )
    {
        return AddIpMatcher(
            an,
            AddressFamily.InterNetwork,
            customMessageGenerator
        );
    }

    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <returns></returns>
    public static IMore<string> Ipv6Address(
        this IAn<string> an
    )
    {
        return an.Ipv6Address(NULL_STRING);
    }

    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<string> Ipv6Address(
        this IAn<string> an,
        string customMessage
    )
    {
        return an.Ipv6Address(() => customMessage);
    }

    /// <summary>
    /// Validates that the provided string is an ip address
    /// </summary>
    /// <param name="an"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<string> Ipv6Address(
        this IAn<string> an,
        Func<string> customMessageGenerator
    )
    {
        return AddIpMatcher(
            an,
            AddressFamily.InterNetworkV6,
            customMessageGenerator
        );
    }


    private static IMore<string> AddIpMatcher(
        IAn<string> an,
        AddressFamily? addressFamily,
        Func<string> customMessageGenerator
    )
    {
        return an.AddMatcher(actual =>
            {
                var passed = IPAddress.TryParse(actual, out var parsed);
                if (addressFamily is not null)
                {
                    passed = passed && parsed.AddressFamily == addressFamily;
                }

                var type = addressFamily switch
                {
                    AddressFamily.InterNetwork => "ipv4 ",
                    AddressFamily.InterNetworkV6 => "ipv6 ",
                    _ => ""
                };

                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected '{actual}' {passed.AsNot()}to be a valid ip {type}address",
                        customMessageGenerator
                    )
                );
            }
        );
    }
}