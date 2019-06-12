using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class EffectTrainingView : TrainingItemView {

    public LineRenderer lineRenderer;
    public RectTransform arrow;
    public Button arrowButton;

    private AttributeTrainingView attributeView;
    private ActionTrainingView actionView;
    
    private Vector3 actionPosition;
    private Vector3 attributePosition;

    private Vector3 actionOffset = new Vector3(-20, 0, -0.5f);
    private Vector3 attributeOffset = new Vector3(20, 0, -0.5f);

    private Color lineColor = new Color(255, 255, 255);
    private float lineWidth = 0.5f;

    private EffectType type;

    public void Start() {
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.SetPositions(new Vector3[2] { Vector3.forward, Vector3.forward });
    }

    public void Update() {
        if (attributeView != null & actionView != null & actionPosition != null & attributePosition != null) {
            if (attributePosition != attributeView.transform.position | actionPosition != actionView.transform.position) {
                AdjustLinePosition();
                AdjustDirectionalArrow();
            }
        }
    }

    public void AdjustLinePosition() {
        actionPosition = actionView.transform.position;
        attributePosition = attributeView.transform.position;
        lineRenderer.SetPositions(new Vector3[2] { actionPosition + actionOffset, attributePosition + attributeOffset });
    }

    public void LinkAttributeToAction(AttributeTrainingView attributeView, ActionTrainingView actionView, EffectType type) {
        this.attributeView = attributeView;
        this.actionView = actionView;
        this.type = type;
    }

    public void AdjustDirectionalArrow() {
        Vector3 vectorToTarget;
        Vector3 pos1;
        Vector3 pos2;
        switch (type) {
            case EffectType.Effect:
                pos1 = attributeView.transform.position + attributeOffset;
                pos2 = actionView.transform.position + actionOffset;
                vectorToTarget = (actionView.transform.position + actionOffset) - (attributeView.transform.position + attributeOffset);
                break;
            case EffectType.Affect:
                pos2 = attributeView.transform.position + attributeOffset;
                pos1 = actionView.transform.position + actionOffset;
                vectorToTarget = (attributeView.transform.position + attributeOffset) - (actionView.transform.position + actionOffset);
                break;
            default: throw new System.Exception("Action type not implemented");
        }
        
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);
        arrow.position = pos1 + (pos2 - pos1) / 1.5f;
    }
}