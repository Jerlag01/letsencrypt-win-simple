<a href="https://zerossl.com">
<picture>
  <!-- Dark mode -->
  <source
    media="(prefers-color-scheme: dark)"
    srcset="https://github.com/win-acme/win-acme/blob/master/win-acme_banner_rounded_dark.png" />
  <!-- Light mode -->
  <source
    media="(prefers-color-scheme: light)"
    srcset="https://github.com/win-acme/win-acme/blob/master/win-acme_banner_rounded_light.png" />
  <!-- Fallback for environments without media queries -->
  <img
    alt="ZeroSSL"
    src="https://github.com/win-acme/win-acme/blob/master/win-acme_banner_rounded_light.png"
    height="auto" />
</picture>
</a>

# win-acme

This is a ACMEv2 client for Windows that aims to be very simple to start with, but powerful enough to grow into almost every scenario.

* A very simple interface to create and install certificates on a local IIS server
* A more advanced interface for many other use cases, including Apache and Exchange
* Automatically creates a scheduled task to renew certificates when needed
* Get certificates with wildcards (*.example.com), international names (证书.example.com), OCSP Must Staple extension (optional).
* Re-use private keys for DANE, use EC crypto or bring your own CSR
* Advanced toolkit for DNS, HTTP and TLS validation: SFTP/FTPS, acme-dns, Azure, Route53, Cloudflare and many more…
* Store your certificates where and how you want them: Windows, IIS Central Store, .pem files, .pfx file or KeyVault
* Compatible with all popular ACME services, including Let’s Encrypt, ZeroSSL, DigiCert, Sectigo, Buypass, Keyon and others…
* Completely unattended operation from the command line
* Other forms of automation through manipulation of .json files
* Write your own Powershell .ps1 scripts to handle installation and validation
* Build your own plugins with C#

## Getting started
Download the .zip file from the download menu, unpack it to a location on your hard disk and run `wacs.exe`.
Alternatively install .NET Core, run `dotnet tool install win-acme --global` and then `wacs.exe`.

## Getting help
If you require assistance please check the manual first before looking for support.


## About this repository
> [!NOTE]
> This repository is officially maintained by <strong>ZeroSSL</strong> as part of our commitment to secure and reliable SSL/TLS solutions. We welcome contributions and feedback from the community!  
> For more information about our services, including free and paid SSL/TLS certificates, visit https://zerossl.com.
<p align="center">
	<a href="https://zerossl.com.com">
		<picture>
			<source media="(prefers-color-scheme: dark)" srcset="https://zerossl.com/assets/images/zerossl_logo_white.svg">
			<source media="(prefers-color-scheme: light)" srcset="https://zerossl.com/assets/images/zerossl_logo.svg">
			<img src="https://zerossl.com/assets/images/zerossl_logo.svg" alt="ZeroSSL" width="256">
		</picture>
	</a>
</p>
