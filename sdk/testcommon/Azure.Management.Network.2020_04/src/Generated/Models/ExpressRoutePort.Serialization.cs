// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.Management.Network.Models
{
    public partial class ExpressRoutePort : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(Identity))
            {
                writer.WritePropertyName("identity");
                writer.WriteObjectValue(Identity);
            }
            if (Optional.IsDefined(Id))
            {
                writer.WritePropertyName("id");
                writer.WriteStringValue(Id);
            }
            if (Optional.IsDefined(Location))
            {
                writer.WritePropertyName("location");
                writer.WriteStringValue(Location);
            }
            if (Optional.IsCollectionDefined(Tags))
            {
                writer.WritePropertyName("tags");
                writer.WriteStartObject();
                foreach (var item in Tags)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteStringValue(item.Value);
                }
                writer.WriteEndObject();
            }
            writer.WritePropertyName("properties");
            writer.WriteStartObject();
            if (Optional.IsDefined(PeeringLocation))
            {
                writer.WritePropertyName("peeringLocation");
                writer.WriteStringValue(PeeringLocation);
            }
            if (Optional.IsDefined(BandwidthInGbps))
            {
                writer.WritePropertyName("bandwidthInGbps");
                writer.WriteNumberValue(BandwidthInGbps.Value);
            }
            if (Optional.IsDefined(Encapsulation))
            {
                writer.WritePropertyName("encapsulation");
                writer.WriteStringValue(Encapsulation.Value.ToString());
            }
            if (Optional.IsCollectionDefined(Links))
            {
                writer.WritePropertyName("links");
                writer.WriteStartArray();
                foreach (var item in Links)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        internal static ExpressRoutePort DeserializeExpressRoutePort(JsonElement element)
        {
            Optional<string> etag = default;
            Optional<ManagedServiceIdentity> identity = default;
            Optional<string> id = default;
            Optional<string> name = default;
            Optional<string> type = default;
            Optional<string> location = default;
            Optional<IDictionary<string, string>> tags = default;
            Optional<string> peeringLocation = default;
            Optional<int> bandwidthInGbps = default;
            Optional<float> provisionedBandwidthInGbps = default;
            Optional<string> mtu = default;
            Optional<ExpressRoutePortsEncapsulation> encapsulation = default;
            Optional<string> etherType = default;
            Optional<string> allocationDate = default;
            Optional<IList<ExpressRouteLink>> links = default;
            Optional<IReadOnlyList<SubResource>> circuits = default;
            Optional<ProvisioningState> provisioningState = default;
            Optional<string> resourceGuid = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("etag"))
                {
                    etag = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("identity"))
                {
                    identity = ManagedServiceIdentity.DeserializeManagedServiceIdentity(property.Value);
                    continue;
                }
                if (property.NameEquals("id"))
                {
                    id = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("type"))
                {
                    type = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("location"))
                {
                    location = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("tags"))
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        dictionary.Add(property0.Name, property0.Value.GetString());
                    }
                    tags = dictionary;
                    continue;
                }
                if (property.NameEquals("properties"))
                {
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        if (property0.NameEquals("peeringLocation"))
                        {
                            peeringLocation = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("bandwidthInGbps"))
                        {
                            bandwidthInGbps = property0.Value.GetInt32();
                            continue;
                        }
                        if (property0.NameEquals("provisionedBandwidthInGbps"))
                        {
                            provisionedBandwidthInGbps = property0.Value.GetSingle();
                            continue;
                        }
                        if (property0.NameEquals("mtu"))
                        {
                            mtu = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("encapsulation"))
                        {
                            encapsulation = new ExpressRoutePortsEncapsulation(property0.Value.GetString());
                            continue;
                        }
                        if (property0.NameEquals("etherType"))
                        {
                            etherType = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("allocationDate"))
                        {
                            allocationDate = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("links"))
                        {
                            List<ExpressRouteLink> array = new List<ExpressRouteLink>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(ExpressRouteLink.DeserializeExpressRouteLink(item));
                            }
                            links = array;
                            continue;
                        }
                        if (property0.NameEquals("circuits"))
                        {
                            List<SubResource> array = new List<SubResource>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(SubResource.DeserializeSubResource(item));
                            }
                            circuits = array;
                            continue;
                        }
                        if (property0.NameEquals("provisioningState"))
                        {
                            provisioningState = new ProvisioningState(property0.Value.GetString());
                            continue;
                        }
                        if (property0.NameEquals("resourceGuid"))
                        {
                            resourceGuid = property0.Value.GetString();
                            continue;
                        }
                    }
                    continue;
                }
            }
            return new ExpressRoutePort(id.Value, name.Value, type.Value, location.Value, Optional.ToDictionary(tags), etag.Value, identity.Value, peeringLocation.Value, Optional.ToNullable(bandwidthInGbps), Optional.ToNullable(provisionedBandwidthInGbps), mtu.Value, Optional.ToNullable(encapsulation), etherType.Value, allocationDate.Value, Optional.ToList(links), Optional.ToList(circuits), Optional.ToNullable(provisioningState), resourceGuid.Value);
        }
    }
}
