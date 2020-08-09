using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class InvaderTests
    {
        private Player player;

        [SetUp]
        public void ResetScene()
        {
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
            player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player")).GetComponent<Player>();
        }

        [UnityTest]
        public IEnumerator Game_stop_resets_player()
        {
            // arrange
            //Player player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player")).GetComponent<Player>();
            player.UpdateState(GameState.Playing, GameState.Stopped);
            Vector3 playerStartPosition = player.transform.position;
            int startingHealth = player.Health;

            // act
            player.transform.position += Vector3.one * 5;
            player.TakeDamage();
            player.UpdateState(GameState.Playing, GameState.Lost);

            yield return null;

            // assert
            Assert.AreEqual(playerStartPosition, player.transform.position);
            Assert.AreEqual(startingHealth, player.Health);
        }
    }
}
