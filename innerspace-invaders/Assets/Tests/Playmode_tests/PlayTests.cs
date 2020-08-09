using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayTests
    {
        PlayerBulletPool pool;

        [SetUp]
        public void SetupPool()
        {
            GameObject playerPool = new GameObject();
            playerPool.AddComponent<PlayerBulletPool>();

            pool = playerPool.GetComponent<PlayerBulletPool>();
            pool.SetNewPrefab(Resources.Load<DefaultBullet>("PlayerBullet"));
        }

        [Test]
        public void Test_pool_creation()
        {
            DefaultBullet bullet = pool.GetPooledObject();

            Assert.IsNotNull(bullet);
            Assert.AreEqual(1, pool.ActiveObjectAmount);                  
        }

        [Test]
        public void Test_pool_returns()
        {
            DefaultBullet bullet = pool.GetPooledObject();

            pool.ReturnToPool(bullet);

            Assert.IsFalse(bullet.isActiveAndEnabled);
            Assert.AreEqual(0, pool.ActiveObjectAmount);
        }  

        [UnityTest]
        public IEnumerator Test_bullet_destruction()
        {
            DefaultBullet bullet = pool.GetPooledObject();

            bullet.Object.SetActive(true);
            yield return new WaitForSeconds(bullet.MaxLifeTime + .1f);

            Assert.IsFalse(bullet.isActiveAndEnabled);
            Assert.AreEqual(pool.ActiveObjectAmount, 0);
        }
        
        [TearDown]
        public void TearDownPool()
        {
            Object.Destroy(pool);
        }
    }
}
