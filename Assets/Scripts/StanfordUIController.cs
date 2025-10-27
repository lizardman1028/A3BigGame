using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StanfordUIController : MonoBehaviour
{
    [Header("Main Clickable Object")]
    [SerializeField] private Image mainClickableImage;
    [SerializeField] private Sprite[] stanfordSprites; // Array of your Stanford sprites
    
    [Header("Background Elements")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] backgroundSprites; // Campus backgrounds
    
    [Header("Animation Settings")]
    [SerializeField] private float clickAnimationScale = 1.1f;
    [SerializeField] private float animationDuration = 0.1f;
    
    [Header("Progress Milestones")]
    [SerializeField] private float[] milestones = {100f, 500f, 1000f, 5000f, 10000f, 50000f, 100000f};
    
    private int currentSpriteIndex = 0;
    private int currentBackgroundIndex = 0;
    private Vector3 originalScale;
    private Coroutine clickAnimationCoroutine;
    
    private void Start()
    {
        if (mainClickableImage != null)
        {
            originalScale = mainClickableImage.transform.localScale;
            
            // Start with Stanford logo or student sprite
            if (stanfordSprites.Length > 0)
            {
                mainClickableImage.sprite = stanfordSprites[0];
            }
        }
        
        // Set initial background
        if (backgroundImage != null && backgroundSprites.Length > 0)
        {
            backgroundImage.sprite = backgroundSprites[0];
        }
    }
    
    private void Update()
    {
        CheckForSpriteUpgrades();
    }
    
    public void OnClickableClicked()
    {
        // Animate the clickable object
        if (clickAnimationCoroutine != null)
        {
            StopCoroutine(clickAnimationCoroutine);
        }
        clickAnimationCoroutine = StartCoroutine(ClickAnimation());
    }
    
    private IEnumerator ClickAnimation()
    {
        if (mainClickableImage == null) yield break;
        
        Transform imageTransform = mainClickableImage.transform;
        Vector3 targetScale = originalScale * clickAnimationScale;
        
        // Scale up
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration / 2f)
        {
            float t = elapsedTime / (animationDuration / 2f);
            imageTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Scale back down
        elapsedTime = 0f;
        while (elapsedTime < animationDuration / 2f)
        {
            float t = elapsedTime / (animationDuration / 2f);
            imageTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        imageTransform.localScale = originalScale;
    }
    
    private void CheckForSpriteUpgrades()
    {
        if (GlobalInjector.inventoryManager == null || stanfordSprites.Length == 0) return;
        
        float currentClicks = GlobalInjector.inventoryManager.Clicks;
        
        // Check if we should upgrade the main sprite based on milestones
        for (int i = milestones.Length - 1; i >= 0; i--)
        {
            if (currentClicks >= milestones[i] && currentSpriteIndex <= i + 1)
            {
                UpdateMainSprite(i + 1);
                break;
            }
        }
        
        // Update background based on progress
        UpdateBackground(currentClicks);
    }
    
    private void UpdateMainSprite(int newIndex)
    {
        if (newIndex >= 0 && newIndex < stanfordSprites.Length && newIndex != currentSpriteIndex)
        {
            currentSpriteIndex = newIndex;
            mainClickableImage.sprite = stanfordSprites[currentSpriteIndex];
            
            // You could add a transition effect here
            Debug.Log($"Upgraded to sprite: {stanfordSprites[currentSpriteIndex].name}");
        }
    }
    
    private void UpdateBackground(float currentClicks)
    {
        if (backgroundSprites.Length == 0) return;
        
        // Change background based on progress milestones
        int targetBackgroundIndex = 0;
        
        if (currentClicks >= 10000f) targetBackgroundIndex = Mathf.Min(3, backgroundSprites.Length - 1);
        else if (currentClicks >= 1000f) targetBackgroundIndex = Mathf.Min(2, backgroundSprites.Length - 1);
        else if (currentClicks >= 100f) targetBackgroundIndex = Mathf.Min(1, backgroundSprites.Length - 1);
        
        if (targetBackgroundIndex != currentBackgroundIndex && backgroundImage != null)
        {
            currentBackgroundIndex = targetBackgroundIndex;
            backgroundImage.sprite = backgroundSprites[currentBackgroundIndex];
            Debug.Log($"Background changed to: {backgroundSprites[currentBackgroundIndex].name}");
        }
    }
    
    // Method to be called by the Clickable script
    public static void TriggerClickAnimation()
    {
        StanfordUIController controller = FindObjectOfType<StanfordUIController>();
        if (controller != null)
        {
            controller.OnClickableClicked();
        }
    }
}