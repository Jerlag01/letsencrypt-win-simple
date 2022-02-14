﻿using PKISharp.WACS.Clients.IIS;
using PKISharp.WACS.DomainObjects;
using PKISharp.WACS.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PKISharp.WACS.UnitTests.Mock.Clients
{
    internal class MockIISClient : IIISClient<MockSite, MockBinding>
    {
        private readonly ILogService _log;

        public MockIISClient(ILogService log, int version = 10)
        {
            _log = log;
            Version = new Version(version, 0);
            MockSites = new[] {
                new MockSite()
                {
                    Id = 1,
                    Name = "example.com",
                    Path = "C:\\wwwroot\\example",
                    Bindings = new[]
                    {
                        new MockBinding()
                        {
                            Host = "test.example.com",
                            Protocol = "http",
                            Port = 80
                        },
                        new MockBinding()
                        {
                            Host = "alt.example.com",
                            Protocol = "http",
                            Port = 80
                        },
                        new MockBinding()
                        {
                            Host = "经/已經.example.com",
                            Protocol = "http",
                            Port = 80
                        },
                        new MockBinding()
                        {
                            Host = "four.example.com",
                            Protocol = "http",
                            Port = 80
                        },
                    }.ToList()
                },
                new MockSite()
                {
                    Id = 2,
                    Name = "contoso.com",
                    Path = "C:\\wwwroot\\contoso",
                    Bindings = new[]
                    {
                        new MockBinding()
                        {
                            Host = "test.contoso.com",
                            Protocol = "http",
                            Port = 80
                        },
                        new MockBinding()
                        {
                            Host = "alt.contoso.com",
                            Protocol = "http",
                            Port = 80
                        },
                        new MockBinding()
                        {
                            Host = "经/已經.contoso.com",
                            Protocol = "http",
                            Port = 80
                        },
                        new MockBinding()
                        {
                            Host = "four.contoso.com",
                            Protocol = "http",
                            Port = 80
                        },
                    }.ToList()
                }
            };
        }

        public Version Version { get; set; }
        public MockSite[] MockSites { get; set; }

        IEnumerable<IIISSite> IIISClient.Sites => Sites;
        public IEnumerable<MockSite> Sites => MockSites;

        public bool HasFtpSites => Sites.Any(x => x.Type == IISSiteType.Ftp);
        public bool HasWebSites => Sites.Any(x => x.Type == IISSiteType.Web);

        public void UpdateHttpSite(IEnumerable<Identifier> identifiers, BindingOptions bindingOptions, byte[]? oldThumbprint)
        {
            var updater = new IISHttpBindingUpdater<MockSite, MockBinding>(this, _log);
            var updated = updater.AddOrUpdateBindings(identifiers, bindingOptions, oldThumbprint);
            if (updated > 0)
            {
                _log.Information("Committing {count} {type} binding changes to IIS while updating site {site}", updated, "https", bindingOptions.SiteId);

            }
            else
            {
                _log.Information("No bindings have been changed while updating site {site}", bindingOptions.SiteId);
            }
        }
        public MockSite GetSite(long id, IISSiteType? type = null) => Sites.First(x => id == x.Id && (type == null || x.Type == type));
        public void UpdateFtpSite(long? FtpSiteId, CertificateInfo newCertificate, CertificateInfo? oldCertificate) { }
        IIISSite IIISClient.GetSite(long id, IISSiteType? type) => GetSite(id, type);

        public IIISBinding AddBinding(MockSite site, BindingOptions bindingOptions) {
            var newBinding = new MockBinding(bindingOptions);
            site.Bindings.Add(newBinding);
            return newBinding;
        } 

        public void UpdateBinding(MockSite site, MockBinding binding, BindingOptions bindingOptions)
        {
            _ = site.Bindings.Remove(binding);
            var updateOptions = bindingOptions
                .WithHost(binding.Host)
                .WithIP(binding.IP)
                .WithPort(binding.Port);
            site.Bindings.Add(new MockBinding(updateOptions));
        }

        public void Refresh()
        {
        }
        public void ReplaceCertificate(CertificateInfo newCertificate, CertificateInfo oldCertificate)
        {
            throw new NotImplementedException();
        }
    }

    internal class MockSite : IIISSite<MockBinding>
    {
        IEnumerable<IIISBinding> IIISSite.Bindings => Bindings;
        public List<MockBinding> Bindings { get; set; } = new List<MockBinding>();
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        IEnumerable<MockBinding> IIISSite<MockBinding>.Bindings => Bindings;
        public IISSiteType Type => IISSiteType.Web;
    }

    internal class MockBinding : IIISBinding
    {
        public MockBinding() { }
        public MockBinding(BindingOptions options)
        {
            Host = options.Host;
            Protocol = "https";
            Port = options.Port;
            CertificateHash = options.Thumbprint;
            CertificateStoreName = options.Store ?? "";
            IP = options.IP;
            SSLFlags = options.Flags;
        }

        public string Host { get; set; } = "";
        public string Protocol { get; set; } = "";
        public int Port { get; set; }
        public string IP { get; set; } = "";
        public byte[]? CertificateHash { get; set; }
        public string CertificateStoreName { get; set; } = "";
        public string BindingInformation
        {
            get
            {
                if (_bindingInformation != null)
                {
                    return _bindingInformation;
                }
                else
                {
                    return $"{IP}:{Port}:{Host}";
                }
            }
            set => _bindingInformation = value;
        }
        private string? _bindingInformation = null;
        public SSLFlags SSLFlags { get; set; }
        public bool Secure => Protocol == "https" || Protocol == "ftps";
    }
}
