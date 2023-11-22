﻿using System;
using System.Globalization;
using DG.Tweening;
using UnityEngine;
using Object = System.Object;

namespace Source.Scripts.Extensions
{
    public static class OtherExtensions
    {
        public static string FormatNumberWithCommas(int value)
        {
            return string.Format(new CultureInfo("en-US"), "{0:N0}", value);
        }
        
        public static void TransformPunchScale(Transform target, float punch = 0.1f, float duration = 1f, int vibrato = 4, float elasticity = 1f, Action callback = null)
        {
            target.DORewind();
            target.DOPunchScale(new Vector3(punch, punch, punch), duration, vibrato, elasticity).OnComplete(() =>  callback?.Invoke());
        }
        
        public static void SaveGame(Object data)
        {
            var @string = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("data", @string);
            PlayerPrefs.Save();
        }
    }
}