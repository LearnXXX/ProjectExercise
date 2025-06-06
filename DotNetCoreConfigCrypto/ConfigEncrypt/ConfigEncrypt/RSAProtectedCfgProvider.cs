using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Xml;

namespace ConfigEncrypt
{
    public class RSAProtectedCfgProvider : ProtectedConfigurationProvider
    {
        private dynamic _rsaProvider = null;

        /// <summary>
        /// フレームワークのライブラリを引く
        /// </summary>
        public RSAProtectedCfgProvider()
        {
            Assembly myAssembly = Assembly.LoadFile($@"{System.AppDomain.CurrentDomain.BaseDirectory}\Lib\System.Configuration.dll");
            _rsaProvider = myAssembly.CreateInstance("System.Configuration.RsaProtectedConfigurationProvider");
        }

        public override string Name => _rsaProvider.Name;

        public override string Description => _rsaProvider.Description;

        //
        // 概要:
        //     Decrypts the passed System.Xml.XmlNode object from a configuration file.
        //
        // パラメーター:
        //   encryptedNode:
        //     The System.Xml.XmlNode object to decrypt.
        //
        // 戻り値:
        //     The System.Xml.XmlNode object containing decrypted data.
        public override XmlNode Decrypt(XmlNode encryptedNode)
        {
            return _rsaProvider.Decrypt(encryptedNode);
        }

        //
        // 概要:
        //     Encrypts the passed System.Xml.XmlNode object from a configuration file.
        //
        // パラメーター:
        //   node:
        //     The System.Xml.XmlNode object to encrypt.
        //
        // 戻り値:
        //     The System.Xml.XmlNode object containing encrypted data.
        public override XmlNode Encrypt(XmlNode node)
        {
            return _rsaProvider.Encrypt(node);
        }

        //
        // 概要:
        //     Initializes the configuration builder.
        //
        // パラメーター:
        //   name:
        //     The friendly name of the provider.
        //
        //   config:
        //     A collection of the name/value pairs representing the provider-specific attributes
        //     specified in the configuration for this provider.
        //
        // 例外:
        //   T:System.ArgumentNullException:
        //     The name of the provider is null.
        //
        //   T:System.ArgumentException:
        //     The name of the provider has a length of zero.
        //
        //   T:System.InvalidOperationException:
        //     An attempt is made to call System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     on a provider after the provider has already been initialized.
        public override void Initialize(string name, NameValueCollection config)
        {
            _rsaProvider.Initialize(name, config);
        }
    }
}
