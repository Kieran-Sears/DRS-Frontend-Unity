﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum ConfigItemTypes {
        Customer,
        Action,
        Attribute,
        Categorical,
        Scalar,
        CategoricalOption,
        Configuration,
        Simulation,
        Effect
    }

    [System.Serializable]
    public enum VarianceType {
        None,
        Increasing,
        Decreasing
    }

    public enum ActionType {
        Customer,
        Agent
    }

    public enum EffectType {
        Effect,
        Affect
    }
}
