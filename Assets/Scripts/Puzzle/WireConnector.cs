using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Puzzle
//todo // Store and collect colour
{ public class WireConnector : MonoBehaviour {
        public bool active;
        [Tooltip("0 = Up, 1 = Down, 2 = Left, 3 = Right")]
        [Range(0, 3)][SerializeField] private int direction;
        [SerializeField] private Sprite[] facing;
        [SerializeField] private SpriteRenderer[] symbols;
        [HideInInspector] public List<Wire> wires;
        [SerializeField] private Light2D greenLight;
        [SerializeField] private Light2D redLight;
        public bool andGate, orGate, nandGate, norGate, xorGate, xnorGate;
        private Wire m_WrongWire;
        private SpriteRenderer SpriteRenderer;
        private static readonly int s_Active = Shader.PropertyToID("_Active");
        private void Awake() {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start() {
            Invoke(nameof(FixWireList), 0);
            SpriteRenderer.sprite = facing[direction];
        }
        void FixWireList() {
            foreach (var w in wires.Where(w => w.button == null)) {
                m_WrongWire = w;
            } wires.Remove(m_WrongWire);
        }
        private void Update() => ActivityCheck();
        private bool ActivityCheck() {
            foreach (var symbol in symbols) {
                symbol.enabled = false;
                symbol.material.SetFloat(s_Active, 0f);
            }
            active = false;
            if (!andGate && !orGate && !nandGate && !norGate && !xorGate && !xnorGate)
                throw new WarningException("No settings have been selected for this " + name);
            if (wires != null) {
                var activeWires = 0;
                var inActiveWires = 0;
                if (andGate) {
                    symbols[0].enabled = true;
                    symbols[1].enabled = true;
                    foreach (var w in wires) {
                        if (w.active)
                            activeWires++;
                        if (activeWires == wires.Count) {
                            active = true;
                            symbols[0].material.SetFloat(s_Active, 1f );
                            symbols[1].material.SetFloat(s_Active, 1f );
                            GreenLight();
                        } else if (activeWires == 1) {
                            symbols[0].material.SetFloat(s_Active, 1f );
                            symbols[1].material.SetFloat(s_Active, 0f );
                            NoLight();
                        } else {
                            symbols[0].material.SetFloat(s_Active, 0f );
                            symbols[1].material.SetFloat(s_Active, 0f );
                            NoLight();
                        }
                    }
                } else if (orGate) {
                    symbols[2].enabled = true;
                    foreach (var w in wires) {
                        if (w.active) {
                            active = true;
                            activeWires++;
                            symbols[2].material.SetFloat(s_Active, 1f );
                            GreenLight();
                        }
                        if (activeWires == 0) {
                            symbols[2].material.SetFloat(s_Active, 0f );
                            NoLight();
                        }
                    }
                } else if (nandGate) {
                    foreach (var w in wires) {
                        if (!w.active)
                            active = true;
                    }
                } else if (norGate) {
                    symbols[3].enabled = true;
                    foreach (var w in wires) {
                        if (w.active) {
                            activeWires++;
                        }
                        if (!w.active)
                            inActiveWires++;
                        if (inActiveWires == wires.Count) active = true;
                        if (activeWires <=0) {
                            symbols[3].material.SetFloat(s_Active, 0f);
                            NoLight();
                        } else {
                            symbols[3].material.SetFloat(s_Active, 1f);
                            RedLight();
                        }
                    }
                } else if (xorGate) {
                    symbols[2].enabled = true;
                    symbols[3].enabled = true;
                    foreach (var w in wires) {
                        if (!w.active)
                            inActiveWires++;
                        if (w.active)
                            activeWires++;
                        if (inActiveWires > 0 && inActiveWires < wires.Count && activeWires > 0 && activeWires < wires.Count) active = true;
                        if (activeWires == 1) {
                            symbols[2].material.SetFloat(s_Active, 1f);
                            GreenLight();
                        } else if (activeWires >= 2)
                        {
                            symbols[3].material.SetFloat(s_Active, 1f);
                            RedLight();
                        } else {
                            symbols[2].material.SetFloat(s_Active, 0f);
                            symbols[3].material.SetFloat(s_Active, 0f);
                            NoLight();
                        }
                    }  
                } else if (xnorGate) {
                    foreach (var w in wires) {
                        if (!w.active)
                            inActiveWires++;
                        if (w.active)
                            activeWires++;
                        if (inActiveWires == wires.Count || activeWires == wires.Count) active = true;
                    }
                } else {
                    foreach (var w in wires) {
                        if (w.active)
                            active = true;
                    }
                }
            }
            return active;
        }

        void GreenLight() {
            greenLight.enabled = true;
            redLight.enabled = false;
        }
        void RedLight() {
            greenLight.enabled = false;
            redLight.enabled = true;
        }
        void NoLight() {
            greenLight.enabled = false;
            redLight.enabled = false;
        }
    }
}
