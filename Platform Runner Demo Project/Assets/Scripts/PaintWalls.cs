using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintWalls : MonoBehaviour
{

    [SerializeField] float paintDelay = 1.5f; // saða sola kaydýrma mekaniðinde geçen süre

    Renderer objectRenderer;
    ProgressIcon progressIcon;

    bool isPainting;
    Color newColor;

    public bool IsPainting { get => isPainting; private set => isPainting = value; } 
    public Color NewColor { get => newColor; private set => newColor = value; }

    public event Action onDone;
    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.SetFloat("_PaintAmount", 0);

        progressIcon = FindObjectOfType<ProgressIcon>();
    }

    public void StartPainting()
    {
        IsPainting = true;
        StartCoroutine(PaintCoroutine());
    }
    public void SetColor(Color colorToPaint)
    {
        NewColor = colorToPaint;
    }
   
    private IEnumerator PaintCoroutine()
    {
        float currentTime = 0f;
        objectRenderer.material.SetColor("_PaintColor", NewColor);
        progressIcon.ToggleIcon(true);

        while (true)
        {
            currentTime += Time.fixedDeltaTime;
            float amount = Mathf.Clamp01(currentTime / paintDelay);
            progressIcon.SetFillAmount(amount);
            objectRenderer.material.SetFloat("_PaintAmount", amount);
            yield return null;
        }
        objectRenderer.material.SetFloat("_PaintAmount", 0);
        objectRenderer.material.SetColor("_PaintColor", NewColor);
        IsPainting = false;
        progressIcon.ToggleIcon(false);
        onDone?.Invoke();

    }

}
