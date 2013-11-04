#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

#endregion

namespace Pikatwo{
    internal class Authenticator : IrcComponent{
        const string _authFile = "auth.json";

        readonly List<AuthUser> _users;

        public Authenticator(){
            _users = new List<AuthUser>();
            LoadAuthFile();
        }

        #region IrcComponent Members

        public ClientInterface IrcInterface { get; set; }

        public void Update(long secsSinceStart){
        }

        #endregion

        public void LoadAuthFile(){
            var sr = new StreamReader(_authFile);
            var users = JsonConvert.DeserializeObject<List<AuthUser>>(sr.ReadToEnd());
            _users.Clear();
            _users.AddRange(users);
        }

        public AuthLevel GetUserAuthLevel(string hostmask){
            var user = _users.Where(u => u.Hostmask.Equals(hostmask)).ToArray();
            if (user.Length == 0){
                return AuthLevel.User;
            }
            return user[0].AuthLevel;
        }

        #region Nested type: AuthUser

        class AuthUser{
            public readonly AuthLevel AuthLevel;
            public readonly string Hostmask;

            public AuthUser(string hostmask, AuthLevel authLevel){
                Hostmask = hostmask;
                AuthLevel = authLevel;
            }
        }

        #endregion
    }
}