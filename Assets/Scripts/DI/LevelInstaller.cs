using System.Collections.Generic;
using Character;
using PlayerProfileSystem;
using UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public sealed class LevelInstaller : MonoInstaller
    {
        [SerializeField] private CharacterDatabase _characterDatabase;
        [SerializeField] private UIManager _uiManager;


        public override void InstallBindings()
        {
            Container.Bind<CharacterDatabase>().FromInstance(_characterDatabase).AsSingle().NonLazy();

            Container.Bind<PlayerProfile>().ToSelf().AsSingle().WithArguments(_characterDatabase).NonLazy();
            Container.Bind<ProfileService>().ToSelf().AsSingle().NonLazy();
            
            Container.Bind<UIManager>().FromInstance(_uiManager).AsCached().NonLazy();
        }
    }
}
